import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import { DatePipe } from '@angular/common'
import {FormatWidth, getLocaleDateTimeFormat} from "@angular/common";

@Component({
  selector: 'app-root',
  templateUrl: './rezervacije.component.html',
  styleUrls: ['./rezervacije.component.css']
})
export class RezervacijeComponent implements OnInit{
  rezervacije_podaci:any;
  nova_rezervacija:any;
  odabrana_rezervacija: any;
  danasnji_datum:any;
  vrsta_rezervacija:string= "All reservations";
  filter_podaci: any="";

  constructor(private httpKlijent: HttpClient, public datepipe:DatePipe) {
  }

  preuzmi_podatke(tipKategorije:any="All")
  {
    if (tipKategorije=="All" && this.filter_podaci==""){
      this.vrsta_rezervacija="All reservations";
      return this.httpKlijent.get(MojConfig.adresa_servera + "/Rezervacija/GetAll").subscribe(x=>{
        this.rezervacije_podaci = x;
      });
    }
    else if(tipKategorije=="Pending" && this.filter_podaci==""){
      this.vrsta_rezervacija="Pending reservations";
      this.httpKlijent.get(MojConfig.adresa_servera+ "/Rezervacija/GetAll").subscribe((x:any)=>{
        this.rezervacije_podaci=x;
        this.rezervacije_podaci=this.rezervacije_podaci.filter((x:any)=>x.status=='new');
      });
      this.rezervacije_podaci=this.rezervacije_podaci.filter((x:any)=>x.status=='new');
      return this.rezervacije_podaci;
    }
    else if(tipKategorije=='Concluded' && this.filter_podaci==""){
      this.vrsta_rezervacija="Concluded reservations";
      this.httpKlijent.get(MojConfig.adresa_servera+ "/Rezervacija/GetAll").subscribe((x:any)=>{
        this.rezervacije_podaci=x;
        this.rezervacije_podaci=this.rezervacije_podaci.filter((x:any)=>x.status=='concluded');
      });
      this.rezervacije_podaci=this.rezervacije_podaci.filter((x:any)=>x.status=='concluded');
      return this.rezervacije_podaci;
    }
    else if (tipKategorije=="All" && this.filter_podaci!=""){
      this.vrsta_rezervacija="All reservations";
      return this.httpKlijent.get(MojConfig.adresa_servera + "/Rezervacija/GetAll").subscribe(x=>{
        this.rezervacije_podaci = this.rezervacije_podaci.filter((x:any)=>x.id.toString().startsWith(this.filter_podaci));;
      });
    }
    else if (tipKategorije=="Concluded" && this.filter_podaci!=""){
      this.vrsta_rezervacija="All reservations";
      return this.httpKlijent.get(MojConfig.adresa_servera + "/Rezervacija/GetAll").subscribe(x=>{
        this.rezervacije_podaci = this.rezervacije_podaci.filter((x:any)=>x.id.toString().startsWith(this.filter_podaci) && x.status=='concluded');;
      });
    }
    else if (tipKategorije=="Pending" && this.filter_podaci!=""){
      this.vrsta_rezervacija="All reservations";
      return this.httpKlijent.get(MojConfig.adresa_servera + "/Rezervacija/GetAll").subscribe(x=>{
        this.rezervacije_podaci = this.rezervacije_podaci.filter((x:any)=>x.id.toString().startsWith(this.filter_podaci) && x.status=='pending');;
      });
    }
  }

  ngOnInit(): void {
    this.nova_rezervacija = {
      datumRezervacije:"2022-11-25",
      napomena:"",
      status:"new",
      korisnikID:1 //za sada defaultni korisnik koji ce imati prikaz svojih rezervacija
    }
    this.odabrana_rezervacija={
      id:null
    }
    this.preuzmi_podatke("All");
  }

  getpodaci() {
    if (this.rezervacije_podaci == null)
      return [];
    return this.rezervacije_podaci;
  }

  Add() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/Rezervacija/Add/", this.nova_rezervacija).subscribe(x=>{
    alert("Uspjesno dodavanje rezervacije");
    window.location.reload();
    });
  }

  Update(id:any) {
    this.odabrana_rezervacija.status="update";
    this.odabrana_rezervacija.datumRezervacije=this.datepipe.transform(this.odabrana_rezervacija.datumRezervacije, 'yyyy-MM-dd');
    console.log(this.odabrana_rezervacija.datumRezervacije);
    this.httpKlijent.post(MojConfig.adresa_servera + "/Rezervacija/Update/"+id,this.odabrana_rezervacija).subscribe(((x:any)=>{
      alert("Uspjesan update rezervacije");
      this.odabrana_rezervacija.id=null;
      window.location.reload();
    }));
  }

  Delete(rezervacija: any){
    this.httpKlijent.post(MojConfig.adresa_servera+"/Rezervacija/Delete/" + rezervacija.id,rezervacija.id).subscribe((x=>{
      alert("Uspjesno brisanje rezervacije");
      window.location.reload();
    }))
  }

}
