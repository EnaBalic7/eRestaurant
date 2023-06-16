import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import { DatePipe } from '@angular/common'
import {FormatWidth, getLocaleDateTimeFormat} from "@angular/common";

@Component({
  selector: 'app-root',
  templateUrl: './pitanja.component.html',
  styleUrls: ['./pitanja.component.css']
})
export class PitanjaComponent implements OnInit{
  pitanja_podaci:any;
  novo_pitanje:any;
  odabrano_pitanje:any;

  constructor(private httpKlijent: HttpClient, public datepipe:DatePipe) {
  }
  ngOnInit(): void {
    this.novo_pitanje = {
      datum:"2022-11-25",
      opis:"",
      status:"unanswered",
      odgovor:"",
      korisnikID:2,//za sada defaultni korisnik koji ce imati prikaz svojih rezervacija
      radnikID:1
    }
    this.odabrano_pitanje={
      id:null,
      datum:null
    }
    this.preuzmipodatke();
  }

  preuzmipodatke(){
    return this.httpKlijent.get(MojConfig.adresa_servera + "/Pitanje/GetAll").subscribe(x=>{
      this.pitanja_podaci = x;});
  }

  getpodaci(){
    if (this.pitanja_podaci == null)
      return [];
    return this.pitanja_podaci;
  }

  Delete(pitanje: any){
    this.httpKlijent.post(MojConfig.adresa_servera+"/Pitanje/Delete/" + pitanje.id,pitanje.id).subscribe((x=>{
      alert("Uspjesno brisanje pitanja");
      window.location.reload();
    }))
  }

  Add() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/Pitanje/Add/", this.novo_pitanje).subscribe(x=>{
      alert("Uspjesno dodavanje pitanja");
      window.location.reload();
    });
  }

  Update(id:any){
    console.log(this.odabrano_pitanje.datum);
    this.odabrano_pitanje.status="update";
    this.odabrano_pitanje.datum=this.datepipe.transform(this.odabrano_pitanje.datum, 'yyyy-MM-dd');
    console.log(this.odabrano_pitanje.datum);
    this.httpKlijent.post(MojConfig.adresa_servera + "/Pitanje/Update/"+id,this.odabrano_pitanje).subscribe(((x:any)=>{
      alert("Uspjesan update pitanja");
      this.odabrano_pitanje.id=null;
      window.location.reload();
    }));
  }
}
