import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {formatDate} from "@angular/common";

@Component({
  selector: 'app-root',
  templateUrl: './izvjestaji.component.html',
  styleUrls: ['./izvjestaji.component.css']
})
export class IzvjestajiComponent implements OnInit {

  search_date: any = "";
  search_date2: any = "";
  izvjestajiPodaci: any = [];
  odabrani_izvjestaj:any;


  constructor(private httpKlijent: HttpClient) {
  }

  ngOnInit(): void {
    this.preuzmi_izvjestaje();
  }

  preuzmi_izvjestaje() {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Izvjestaj/GetAll?datum1="+this.search_date+"&datum2="+this.search_date2).subscribe((x:any)=>{
        this.izvjestajiPodaci=x;
      }
    )
  }

  formatdate(datum: any) {
    const format = "dd/MM/yyyy";
    const locale = "en-US";
    return formatDate(datum, format, locale);
  }

  odaberi_izvjestaj(izvjestaj:any){

    this.odabrani_izvjestaj=izvjestaj;
  }

  scrollUp() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}
