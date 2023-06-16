import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";

@Component({
  selector: 'app-root',
  templateUrl: './proizvodi.component.html',
  styleUrls: ['./proizvodi.component.css']
})
export class ProizvodiComponent implements OnInit{
  proizvodi_podaci:any = [];
  filter_proizvod: any;
  odabrani_proizvod: any;
  novi_proizvod: any;
  kategorijePodaci: any;
  deleteActivated: boolean = false;
  proizvod_za_delete: any;
  newProductAdded: boolean = false;
  productEdited: boolean = false;
  product_category_name: string="All products";
  search_by_name: string = "";
  previewActivated:boolean=false;
  isChangedName:boolean=false;
  isChangedPrice:boolean=false;
  isChangedUnit:boolean=false;

constructor(private httpKlijent: HttpClient) {
}
  ngOnInit(): void {
    this.novi_proizvod = {
      nazivProizvoda:"",
      pocetnaCijena:0,
      opis:"",
      recenzija:0,
      slikaNovaBase64:"",
      jedinicaMjere:0,
      proizvodiKategorijeID:1
    }
    this.preuzmi_proizvode();
    this.preuzmi_kategorije();
  }
  preuzmi_proizvode(categoryID:number=0){

    if(categoryID!=0 && this.search_by_name==""){
      this.httpKlijent.get(MojConfig.adresa_servera + "/Proizvod/GetAll").subscribe((x: any) => {
        this.proizvodi_podaci = x.filter((y:any)=> y.proizvodiKategorijeID==categoryID);
      })
    }
    else if(categoryID==0 && this.search_by_name!=""){
      this.httpKlijent.get(MojConfig.adresa_servera + "/Proizvod/GetAll").subscribe((x: any) => {
        this.proizvodi_podaci = x.filter((y:any)=> y.nazivProizvoda.toLowerCase().includes(this.search_by_name.toLowerCase()));
      })
    }
    else if(categoryID!=0 && this.search_by_name!=""){
      this.httpKlijent.get(MojConfig.adresa_servera + "/Proizvod/GetAll").subscribe((x: any) => {
        this.proizvodi_podaci = x.filter((y:any)=> y.nazivProizvoda.toLowerCase().includes(this.search_by_name.toLowerCase())
          && y.proizvodiKategorijeID==categoryID)
      })
    }
    else{
      this.httpKlijent.get(MojConfig.adresa_servera + "/Proizvod/GetAll").subscribe((x: any) => {
        this.proizvodi_podaci=x;
      })
    }
  }

preuzmi_kategorije(){
  this.httpKlijent.get(MojConfig.adresa_servera+"/ProizvodiKategorije/GetAll").subscribe((x:any)=>{
    this.kategorijePodaci = x;
  });
}
  getProizvodi() {
    if (this.proizvodi_podaci==null)
      return [];
    return this.proizvodi_podaci.filter((x:any)=>x.nazivProizvoda.toLowerCase().includes(this.filter_proizvod.toLowerCase()));
  }

    Delete(){
      this.httpKlijent.post(MojConfig.adresa_servera + "/Proizvod/Delete/" + this.proizvod_za_delete.proizvodID, this.proizvod_za_delete.proizvodID).subscribe((x => {
        this.preuzmi_proizvode()
      }))
    }


  Add() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/Proizvod/Add/", this.novi_proizvod).subscribe(x => {
      this.preuzmi_proizvode();
      this.odabrani_proizvod=null;

    });
  }

  Update() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/Proizvod/Update/"+this.odabrani_proizvod.proizvodID, this.odabrani_proizvod).subscribe(x => {
      this.preuzmi_proizvode();
      this.productEdited=true;
    });
  }

  promptBg(onOff:boolean){
    var bg = document.getElementById("bg");
    if(onOff==true)
    { // @ts-ignore
      bg.style.display="block";
    }
    else{
      // @ts-ignore
      bg.style.display="none";
    }
  }

  get_slika(x: any) { //get slika za odabrani_proizvod
    return MojConfig.adresa_servera + "/Proizvod/GetSlika/" + x.proizvodID;
  }

  generisiPreviewZaNoviProizvod() {
    // @ts-ignore
    var file = document.getElementById("newImageInputID").files[0];

    if(file){
      var reader = new FileReader();
      let this2 = this;
      reader.onload = function () {
        // @ts-ignore
        this2.slikaBase64string = reader.result.toString();
        this2.novi_proizvod.slikaNovaBase64 = this2.slikaBase64string;
      }
      reader.readAsDataURL(file);
    }
  }

  generisiPreviewZaPostojeciProizvod() {
    // @ts-ignore
    var file = document.getElementById("existingImageInputID").files[0];

    if(file){
      var reader = new FileReader();
      let this2 = this;
      reader.onload = function () {
        // @ts-ignore
        this2.slikaBase64string = reader.result.toString();
        this2.odabrani_proizvod.slikaNovaBase64 = this2.slikaBase64string;
      }
        reader.readAsDataURL(file);
      }
  }
  slikaBase64string: string = "";

  reloadPage() {
    location.reload();
  }

  scrollUp() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  odaberi_proizvod(x: any) {
    this.odabrani_proizvod={
      proizvodID:x.proizvodID,
      nazivProizvoda:x.nazivProizvoda,
      pocetnaCijena:x.pocetnaCijena,
      opis:x.opis,
      recenzija:x.recenzija,
      jedinicaMjere:x.jedinicaMjere,
      proizvodiKategorijeID:x.proizvodiKategorijeID,
      slikaNovaBase64:x.slikaPostojeca,
    }
  }
}


