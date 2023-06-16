import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {formatDate} from "@angular/common";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-root',
  templateUrl: './kuponi.component.html',
  styleUrls: ['./kuponi.component.css'],
  providers:[DatePipe]
})
export class KuponiComponent implements OnInit {

  search_by_discount: any = "";
  kuponiPodaci: any;
  trenutniDatum: any = new Date();
  datumDeakt: any;
  noviKuponObj: any = {
    datumAktivacije: formatDate(this.trenutniDatum, "yyyy-MM-dd", "en-US"),
    datumDeaktivacije: formatDate(this.trenutniDatum, "yyyy-MM-dd", "en-US"),
    popust: 1,
    deaktiviran: false
  };
  kuponAdded: boolean = false;
  kuponUpdated: boolean = false;
  deleteActivated: boolean = false;
  odabrani_kupon: any = null;
  isChangedActiveUntil: boolean = false;

  constructor(private httpKlijent: HttpClient, private datePipe: DatePipe) {
    this.trenutniDatum = datePipe.transform(this.trenutniDatum, "yyyy-MM-dd");
  }

  ngOnInit(): void {
    this.preuzmi_kupone();

  }

  preuzmi_kupone(search: string = 'All') {

    if (search == 'All' && this.search_by_discount != '') {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Kupon/GetAll").subscribe((x: any) => {
        this.kuponiPodaci = x.filter((o: any) => o.popust.toString().startsWith(this.search_by_discount));
      });
    } else if (search == 'Active' && this.search_by_discount == '') {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Kupon/GetAll").subscribe((x: any) => {
        this.kuponiPodaci = x.filter((o: any) => o.deaktiviran == false);
      });
    } else if (search == 'Active' && this.search_by_discount != '') {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Kupon/GetAll").subscribe((x: any) => {
        this.kuponiPodaci = x.filter((o: any) => o.deaktiviran == false && o.popust.toString().startsWith(this.search_by_discount));
      });
    } else if (search == 'Expired' && this.search_by_discount == '') {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Kupon/GetAll").subscribe((x: any) => {
        this.kuponiPodaci = x.filter((o: any) => o.deaktiviran == true);
      });
    } else if (search == 'Expired' && this.search_by_discount != '') {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Kupon/GetAll").subscribe((x: any) => {
        this.kuponiPodaci = x.filter((o: any) => o.deaktiviran == true && o.popust.toString().startsWith(this.search_by_discount));
      });
    }
    else {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Kupon/GetAll").subscribe((x: any) => {
        this.kuponiPodaci = x;
      });
    }
  }

  formatdate(datum: any) {
    const format = "dd/MM/yyyy";
    const locale = "en-US";
    return formatDate(datum, format, locale);
  }

  isExpired() {
    return this.datumDeakt < this.trenutniDatum;
  }

  addKupon() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/Kupon/Add", this.noviKuponObj).subscribe((x: any) => {
      this.promptBg(true);
      this.kuponAdded = true;
      this.preuzmi_kupone();
    });
  }

  updateKupon() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/Kupon/Update", this.odabrani_kupon).subscribe((x: any) => {
      this.promptBg(true);
      this.kuponUpdated = true;
      this.preuzmi_kupone();
    });
  }
  odaberiKupon(kupon:any){
    this.odabrani_kupon={
      kuponID:kupon.kuponID,
      datumAktivacije: formatDate(kupon.datumAktivacije, "yyyy-MM-dd", "en-US"),
      datumDeaktivacije: formatDate(kupon.datumDeaktivacije, "yyyy-MM-dd", "en-US"),
      popust: kupon.popust,
      deaktiviran: kupon.deaktiviran
    }
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

  deleteCoupon(){
    this.httpKlijent.post(MojConfig.adresa_servera + "/Kupon/Delete/"+this.odabrani_kupon.kuponID, this.odabrani_kupon.kuponID).subscribe((x: any) => {
      this.preuzmi_kupone();
    });
  }

  scrollUp() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}
