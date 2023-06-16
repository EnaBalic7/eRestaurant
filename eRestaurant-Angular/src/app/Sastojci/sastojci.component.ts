import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";

@Component({
  selector: 'app-root',
  templateUrl: './sastojci.component.html',
  styleUrls: ['./sastojci.component.css']
})
export class SastojciComponent implements OnInit {
  search_by_name: string = "";
  sastojci_podaci: any;
  odabrani_sastojak: any;
  novi_sastojak: any;
  newIngredientAdded: boolean = false;
  kategorijePodaci: any;
  dobavljaciPodaci: any;
  ingredientEdited: boolean = false;
  sastojak_za_delete: any;
  deleteActivated: boolean = false;
  order_activated: boolean = false;
  ingredientOrdered: boolean = false;
  isChangedName: boolean = false;
  isChangedQty: boolean = false;

  constructor(private httpKlijent: HttpClient) {
  }

  ngOnInit(): void {
    this.novi_sastojak = {
      naziv:'',
      kolicinaNaStanju:0,
      dobavljacID:2,
      sastojciKategorijeID:3
    }
    this.preuzmi_sastojke();
    this.preuzmi_kategorije();
    this.preuzmi_dobavljace();
  }

  preuzmi_sastojke(categoryName:string='All'){

    if(categoryName!='All' && this.search_by_name==""){
      if(categoryName=='In stock'){
        this.httpKlijent.get(MojConfig.adresa_servera+ "/Sastojci/GetAll").subscribe((x:any)=>{
          this.sastojci_podaci=x.filter((y:any)=> y.kolicinaNaStanju>0);
        })
      }
      else{
        this.httpKlijent.get(MojConfig.adresa_servera+ "/Sastojci/GetAll").subscribe((x:any)=>{
          this.sastojci_podaci=x.filter((y:any)=> y.kolicinaNaStanju==0);
        })
      }

    }
    else if(categoryName=='All' && this.search_by_name!=""){
      this.httpKlijent.get(MojConfig.adresa_servera+ "/Sastojci/GetAll").subscribe((x:any)=>{
        this.sastojci_podaci=x.filter((y:any)=> y.naziv.toLowerCase().includes(this.search_by_name.toLowerCase()));
      })
    }
    else if(categoryName!='All' && this.search_by_name!=""){
      if(categoryName=='In stock') {
        this.httpKlijent.get(MojConfig.adresa_servera + "/Sastojci/GetAll").subscribe((x: any) => {
          this.sastojci_podaci = x.filter((y: any) => y.naziv.toLowerCase().includes(this.search_by_name.toLowerCase())
            && y.kolicinaNaStanju>0
          );
        })
      }
      else{
        this.httpKlijent.get(MojConfig.adresa_servera + "/Sastojci/GetAll").subscribe((x: any) => {
          this.sastojci_podaci = x.filter((y: any) => y.naziv.toLowerCase().includes(this.search_by_name.toLowerCase())
            && y.kolicinaNaStanju==0
          );
        })
      }
    }
    else{
      this.httpKlijent.get(MojConfig.adresa_servera + "/Sastojci/GetAll").subscribe((x: any) => {
        this.sastojci_podaci = x;
      })
    }
  }

  Add() {
    this.httpKlijent.post(MojConfig.adresa_servera+"/Sastojci/Add", this.novi_sastojak).subscribe((x:any)=>{
      this.preuzmi_sastojke();
      this.odabrani_sastojak=null;
    })
  }

  private preuzmi_kategorije() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/SastojciKategorije/GetAll").subscribe((x:any)=>{
      this.kategorijePodaci=x;
    })
  }

  private preuzmi_dobavljace() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Dobavljac/GetAll").subscribe((x:any)=>{
      this.dobavljaciPodaci=x;
    })
  }

  Update() {
    this.httpKlijent.post(MojConfig.adresa_servera+"/Sastojci/Update/"+this.odabrani_sastojak.sastojakID, this.odabrani_sastojak).subscribe((x:any)=>{
      this.preuzmi_sastojke();
    })
  }

  Delete() {
    this.httpKlijent.post(MojConfig.adresa_servera+"/Sastojci/Delete/"+this.sastojak_za_delete.sastojakID, this.odabrani_sastojak).subscribe((x:any)=>{
      this.preuzmi_sastojke();
    })
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

  scrollUp() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}
