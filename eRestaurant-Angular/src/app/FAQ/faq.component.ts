import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {formatDate} from "@angular/common";

@Component({
  selector: 'app-root',
  templateUrl: './faq.component.html',
  styleUrls: ['./faq.component.css']
})
export class FAQComponent implements OnInit {

  search_term:any="";
  pitanjaPodaci:any=[];
  odabrano_pitanje:any;
  answered:boolean=false;

  constructor(private httpKlijent: HttpClient) {
  }

  ngOnInit(): void {
    this.preuzmi_pitanja();
  }
  preuzmi_pitanja(search:any="All"){
    if (search == 'All' && this.search_term != '') {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Pitanje/GetAll").subscribe((x: any) => {
        this.pitanjaPodaci = x.filter((o: any) => o.opis.toString().includes(this.search_term));
      });
    } else if (search == 'Answered' && this.search_term == '') {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Pitanje/GetAll").subscribe((x: any) => {
        this.pitanjaPodaci = x.filter((o: any) => o.odgovor!='');
      });
    } else if (search == 'Answered' && this.search_term != '') {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Pitanje/GetAll").subscribe((x: any) => {
        this.pitanjaPodaci = x.filter((o: any) => o.odgovor!='' && o.opis.toString().includes(this.search_term));
      });
    } else if (search == 'Unanswered' && this.search_term == '') {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Pitanje/GetAll").subscribe((x: any) => {
        this.pitanjaPodaci = x.filter((o: any) => o.odgovor=='');
      });
    } else if (search == 'Unanswered' && this.search_term != '') {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Pitanje/GetAll").subscribe((x: any) => {
        this.pitanjaPodaci = x.filter((o: any) => o.odgovor=='' && o.opis.toString().startsWith(this.search_term));
      });
    }
    else {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Pitanje/GetAll").subscribe((x: any) => {
        this.pitanjaPodaci = x;
      });
    }
  }
  formatdate(datum: any) {
    const format = "dd/MM/yyyy";
    const locale = "en-US";
    return formatDate(datum, format, locale);
  }

  answer(){
    this.httpKlijent.post(MojConfig.adresa_servera + "/Pitanje/Answer/"+this.odabrano_pitanje.pitanjeID,this.odabrano_pitanje).subscribe((x: any) => {
        this.answered=true;
        this.promptBg(true);
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

  scrollUp() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

}
