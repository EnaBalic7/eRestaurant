import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import { DatePipe } from '@angular/common'
import {FormatWidth, getLocaleDateTimeFormat} from "@angular/common";

@Component({
  selector: 'app-root',
  templateUrl: './uplate.component.html',
  styleUrls: ['./uplate.component.css']
})

export class UplateComponent implements OnInit {
  uplate_podaci:any;
  odabrana_uplata:any;

  constructor(private httpKlijent: HttpClient, public datepipe: DatePipe) {
  }

  ngOnInit(): void {
    this.odabrana_uplata={
      id:null
    }
    var blok= document.querySelector(".cuplata") as HTMLDivElement;
    blok.style.display="none";
    this.preuzmipodatke();
  }

  getpodaci(){
    if (this.uplate_podaci==null)
      return [];
    return this.uplate_podaci;
  }

  preuzmipodatke(){
    return this.httpKlijent.get(MojConfig.adresa_servera + "/Uplata/GetAll").subscribe(x=>{
      this.uplate_podaci = x;});
  }
  Ok(){
    var blok= document.querySelector(".cuplata") as HTMLDivElement;
    blok.style.display="none";
  }
  ViewDetails(x:any){
    this.odabrana_uplata=x;
    var blok= document.querySelector(".cuplata") as HTMLDivElement;
    blok.style.display="flex";
  }
}
