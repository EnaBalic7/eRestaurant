import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {formatDate} from "@angular/common";
import * as L from 'leaflet';
import 'leaflet-routing-machine';

@Component({
  selector: 'app-root',
  templateUrl: './dostave.component.html',
  styleUrls: ['./dostave.component.css']
})
export class DostaveComponent implements OnInit {

  search_term:any = "";
  narudzbePodaci:any;
  narudzbaInfoPodaci:any = null;
  map : any;
  routingControl: any;

  constructor(private httpKlijent: HttpClient) {
  }

  ngOnInit(): void {
    this.preuzmi_narudzbe();
    this.map = L.map('map').setView([43.33894, 17.81504], 20);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; OpenStreetMap contributors',
      tileSize: 256
    }).addTo(this.map);
  }

  preuzmi_narudzbe(search:any="All"){
    if (search == 'All' && this.search_term != '') {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Narudzba/GetAll?onlineOnly=true&readyOnly=true").subscribe((x: any) => {
        this.narudzbePodaci = x.filter((y:any)=>(y.korisnik.osoba.ime+' '+y.korisnik.osoba.prezime).toLowerCase().includes(this.search_term.toLowerCase()) );
      });
    } else if (search == 'Active' && this.search_term == '') {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Narudzba/GetAll?onlineOnly=true&readyOnly=true").subscribe((x: any) => {
        this.narudzbePodaci = x.filter((y:any)=>y.statusNarudzbe!='Delivered');
      });
    } else if (search == 'Active' && this.search_term != '') {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Narudzba/GetAll?onlineOnly=true&readyOnly=true").subscribe((x: any) => {
        this.narudzbePodaci = x.filter((y:any)=>y.statusNarudzbe!='Delivered' && (y.korisnik.osoba.ime+' '+y.korisnik.osoba.prezime).toLowerCase().includes(this.search_term.toLowerCase()));
      });
    } else if (search == 'Delivered' && this.search_term == '') {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Narudzba/GetAll?onlineOnly=true&readyOnly=true").subscribe((x: any) => {
        this.narudzbePodaci = x.filter((y:any)=>y.statusNarudzbe=='Delivered');
      });
    } else if (search == 'Delivered' && this.search_term != '') {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Narudzba/GetAll?onlineOnly=true&readyOnly=true").subscribe((x: any) => {
        this.narudzbePodaci = x.filter((y:any)=>y.statusNarudzbe=='Delivered' && (y.korisnik.osoba.ime+' '+y.korisnik.osoba.prezime).toLowerCase().includes(this.search_term.toLowerCase()));
      });
    }
    else {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Narudzba/GetAll?onlineOnly=true&readyOnly=true").subscribe((x: any) => {
        this.narudzbePodaci = x;
      });
    }
    setTimeout((x:any) => this.change_status_color(),80);
  }

  formatdate(datum: any) {
    const format = "dd/MM/yyyy";
    const locale = "en-US";
    return formatDate(datum, format, locale);
  }

  convertStatus(status:number): string{
    switch (status){
      case 0: return 'Is being prepared'
      case 1: return 'Prepared'
      case 2: return 'Is being delivered'
      case 3: return 'Delivered'
    }
      return '';
  }

  change_order_status(order:any){
    this.httpKlijent.post(MojConfig.adresa_servera+"/Narudzba/Update/"+order.narudzbaID, order).subscribe((x:any)=>{
      this.preuzmi_narudzbe();
    })
  }

  get_narudzba_info(id:number){

    var FIT = {
      orderID:19,
      orderAddress:'Fakultet Informacijskih Tehnologija, Kralja Tomislava, Sjeverni logor, Mostar',
      totalCost:50,
      paymentType:'Cash',
      message:'I have nothing to say to you.',
      orderDetails:'3 Pizzas, 2 pumpkin spice cakes, 3 thyme teas'
    }

    var OSBP = {
      orderID:19,
      orderAddress:'Osnovna Škola Bijelo Polje',
      totalCost:50,
      paymentType:'Cash',
      message:'I have nothing to say to you.',
      orderDetails:'3 Pizzas, 2 pumpkin spice cakes, 3 thyme teas'
    }

    var FaultyInfo = {
      orderID:19,
      orderAddress:'-----',
      totalCost:50,
      paymentType:'Cash',
      message:'I have nothing to say to you.',
      orderDetails:'3 Pizzas, 2 pumpkin spice cakes, 3 thyme teas'
    }

      this.narudzbaInfoPodaci = OSBP;
  }

  change_status_color(){
    var selectElements = document.getElementsByClassName("orderStatus");
    // @ts-ignore
    for (const x of selectElements) {
      switch (x.value){
        case 'Prepared': x.classList.add("pripremljena"); break;
        case 'Delivered': x.classList.add("dostavljena"); break;
        case 'Is being delivered': x.classList.add("uDostavi"); break;
      }
    }
  }

  showRoute() {
    var url = "https://nominatim.openstreetmap.org/search?format=json&q=" + encodeURIComponent(this.narudzbaInfoPodaci.orderAddress);
    var start = L.latLng(43.33894, 17.81504); // Lokacija restorana koji koristi aplikaciju
    var this2 = this;

    fetch(url)
      .then(function (response) {
        return response.json();
      })
      .then(function (json) {
        if (json.length > 0) {
          var result = json[0];
          var end = L.latLng(result.lat, result.lon); // Lokacija kupca

          var routingOptions = {
            waypoints: [
              start,
              end
            ],
            router: new L.Routing.OSRMv1({
              serviceUrl: 'https://router.project-osrm.org/route/v1'
            }),

            createMarker: function (i: any, waypoint: any, n: any) {
              return L.marker(waypoint.latLng, {
                draggable: true,
                icon: L.icon({
                  iconUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.3/images/marker-icon.png',
                  shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.9.3/images/marker-shadow.png'
                })
              });
            }
          };

          if (this2.routingControl) {
            this2.map.removeControl(this2.routingControl);
          }

          this2.routingControl = L.Routing.control(routingOptions).addTo(this2.map);

          this2.map.setView([43.33900, 17.80955], 20);
        } else {
          L.popup()
            .setLatLng(start)
            .setContent("Address '" + this2.narudzbaInfoPodaci.orderAddress + "' not found")
            .openOn(this2.map);
        }
      })
      .catch(function (error) {
        console.log(error);
      });
  }
}
