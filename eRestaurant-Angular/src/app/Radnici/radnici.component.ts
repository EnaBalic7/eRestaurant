import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {formatDate} from "@angular/common";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-root',
  templateUrl: './radnici.component.html',
  styleUrls: ['./radnici.component.css'],
  providers: [DatePipe]
})
export class RadniciComponent implements OnInit {
  search_by_name: string = "";
  radnici_podaci: any = [];
  odabrani_radnik: any;
  novi_radnik: any;
  newEmployeeAdded: boolean = false;
  employeeEdited: boolean = false;
  deleteActivated: boolean = false;
  addNewEmployee: boolean = false;
  addNewEmployee2: boolean = false;
  novaOsobaObj: any;
  noviRadnikObj: any;
  trenutniDatum: any = new Date();
  lastAddedOsobaID: any;
  editEmployee: boolean = false;
  editEmployee2: boolean = false;
  odabrana_osoba: any;
  radnik_za_delete: any;
  isChangedFName: boolean = false;
  isChangedLName: boolean = false;
  isChangedEmail: boolean = false;
  isChangedPhone: boolean = false;
  isChangedBirthDate: boolean = false;
  isChangedPosition: boolean = false;
  isChangedEmpDate: boolean = false;
  isChangedFireDate: boolean = false;

  constructor(private httpKlijent: HttpClient, private datePipe: DatePipe) {
    this.trenutniDatum = datePipe.transform(this.trenutniDatum, "yyyy-MM-dd");
  }

  ngOnInit(): void {
    this.preuzmi_radnike();
    this.novaOsoba();
    this.noviRadnik();
  }

  preuzmi_radnike() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Radnik/GetAll").subscribe((x: any) => {
      this.radnici_podaci = x.filter((s: any) => (s.osoba.ime + " " + s.osoba.prezime).toLowerCase().includes(this.search_by_name.toLowerCase())
        || (s.osoba.prezime + " " + s.osoba.ime).toLowerCase().includes(this.search_by_name.toLowerCase()));
    });
  }

  formatdate(datum: any) {
    const format = "dd/MM/yyyy";
    const locale = "en-US";
    return formatDate(datum, format, locale);
  }

  getEndDate(x: any){
    if(x.datumZaposlenja < x.datumZavrsetkaRadnogOdnosa)
      return this.formatdate(x.datumZavrsetkaRadnogOdnosa);
    else
      return '';
  }

  promptBg(onOff: boolean) {
    var bg = document.getElementById("bg");
    if (onOff == true) { // @ts-ignore
      bg.style.display = "block";
    } else {
      // @ts-ignore
      bg.style.display = "none";
    }
  }

  novaOsoba() {
    this.novaOsobaObj = {
      ime: '',
      prezime: '',
      datumRodenja: '',
      brojTelefona: '',
      email: '',
      username: "username",
      password: "password",
      slika: ''
    }
  }

  addOsoba() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/Osoba/Add/", this.novaOsobaObj).subscribe(x => {

    });
  }

  noviRadnik() {
    this.noviRadnikObj = {
      datumZaposlenja: this.trenutniDatum,
      datumZavrsetkaRadnogOdnosa: this.trenutniDatum,
      zvanje: '',
      jmbg: '',
      spol: '',
      pozicija: '',
      osobaID: 1
    }
  }

  addRadnik() {

    this.addOsoba();

    setTimeout(() => {
      this.httpKlijent.post(MojConfig.adresa_servera + "/Radnik/Add/", this.noviRadnikObj).subscribe(x => {
        this.newEmployeeAdded = true;
        this.preuzmi_radnike();
      });
    }, 1000)
  }

  odaberiRadnika(radnik: any) {
    this.odabrana_osoba = {
      osobaID:radnik.osoba.osobaID,
      ime: radnik.osoba.ime,
      prezime: radnik.osoba.prezime,
      datumRodenja: formatDate(radnik.osoba.datumRodenja, "yyyy-MM-dd", "en-US"),
      brojTelefona: radnik.osoba.brojTelefona,
      email: radnik.osoba.email,
      username: radnik.osoba.username,
      password: radnik.osoba.password,
      slika: radnik.osoba.slika
    }

    this.odabrani_radnik = {
      radnikID:radnik.radnikID,
      datumZaposlenja: formatDate(radnik.datumZaposlenja, "yyyy-MM-dd", "en-US"),
      datumZavrsetkaRadnogOdnosa: formatDate(radnik.datumZavrsetkaRadnogOdnosa, "yyyy-MM-dd", "en-US"),
      zvanje: radnik.zvanje,
      jmbg: radnik.jmbg,
      spol: radnik.spol,
      pozicija: radnik.pozicija,
      osobaID: radnik.osobaID,
      osoba: this.odabrana_osoba
    }
  }

  updateOsoba() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/Osoba/Update/" + this.odabrana_osoba.osobaID, this.odabrana_osoba).subscribe((x: any) => {

    });
  }

  updateRadnik() {
    this.updateOsoba();

    setTimeout(() => {
      this.httpKlijent.post(MojConfig.adresa_servera + "/Radnik/Update/" + this.odabrani_radnik.radnikID, this.odabrani_radnik).subscribe(x => {
        this.employeeEdited = true;
        this.preuzmi_radnike();
      });
    }, 1000)
  }

  odaberiZaDelete(radnik: any) {
    this.radnik_za_delete = radnik;
  }

  Delete() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/Radnik/Delete", this.radnik_za_delete).subscribe((x => {
      this.preuzmi_radnike();
    }))
  }


  deselect() {
    this.odabrana_osoba = {
      ime:'',
      prezime:'',
      datumRodenja: '',
      brojTelefona: '',
      email: '',
      username: "username",
      password: "password",
      slika: ''
    }

    this.odabrani_radnik = {
      datumZaposlenja: this.trenutniDatum,
      datumZavrsetkaRadnogOdnosa: this.trenutniDatum,
      zvanje: '',
      jmbg: '',
      spol: '',
      pozicija: '',
      osobaID: 1
    }
  }
}

