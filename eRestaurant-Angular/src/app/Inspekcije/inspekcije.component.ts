import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {formatDate} from "@angular/common";

@Component({
  selector: 'app-root',
  templateUrl: './inspekcije.component.html',
  styleUrls: ['./inspekcije.component.css']
})
export class InspekcijeComponent implements OnInit {

  search_date: any = "";
  search_date2: any = "";
  inspekcijePodaci: any = [];
  odabrana_inspekcija: any;
  novaInspekcijaObj:any;
  trenutni_datum:any=new Date();
  deleteActivated:boolean=false;
  newInspectionAdded:boolean=false;
  inspectionEdited:boolean=false;
  pdfURL: string = MojConfig.adresa_servera + "/SanitarnaDeratizacija/GetPDF/";
  pdfFileBase64 : string = "";
  currentPage : number = 1;
  isChangedCode:boolean = false;
  isChangedDatePerf:boolean = false;
  isChangedDateCert:boolean = false;


  constructor(private httpKlijent: HttpClient) {
  }

  ngOnInit(): void {
    this.preuzmi_inspekcije();
    this.nova_inspekcija();
  }

  public pageNumbersArray() : number[]{
    let result = [];

    for(let i = 0; i < this.TotalPages(); i++){
      result.push(i + 1);
    }
    return result;
  }

  private TotalPages(){
    if(this.inspekcijePodaci != null)
      return this.inspekcijePodaci!.totalPages;

    return 1;
  }

  goToPage(p: number) {
    this.currentPage = p;
    this.preuzmi_inspekcije();
  }

  preuzmi_inspekcije(){
    this.httpKlijent.get(MojConfig.adresa_servera+"/SanitarnaDeratizacija/GetAll?datum1="+this.search_date+"&datum2="+this.search_date2+"&pageNumber="+this.currentPage).subscribe((x:any)=>{
        this.inspekcijePodaci=x;
      }
    )
  }

  nova_inspekcija(){
    this.novaInspekcijaObj={
      datumInspekcije:formatDate(this.trenutni_datum,"yyyy-MM-dd","en-US"),
      brojUgovora:'',
      datumOvjere:formatDate(this.trenutni_datum,"yyyy-MM-dd","en-US"),
      dodatnaNapomena:'',
      radnikID:3,
      pdf:'',
      prilozenPDF:false
    }
  }

  addInspekcija(){
    this.httpKlijent.post(MojConfig.adresa_servera+"/SanitarnaDeratizacija/Add",this.novaInspekcijaObj).subscribe(
      (x:any)=>{
        this.newInspectionAdded=true;
        this.promptBg(true);
          this.preuzmi_inspekcije();
      }
    )
  }

  odaberi_inspekciju(inspekcija:any){
    this.odabrana_inspekcija={
      sanitarnaDeratizacijaID:inspekcija.sanitarnaDeratizacijaID,
      datumInspekcije:formatDate(inspekcija.datumInspekcije,"yyyy-MM-dd","en-US"),
      brojUgovora:inspekcija.brojUgovora,
      datumOvjere:formatDate(inspekcija.datumOvjere,"yyyy-MM-dd","en-US"),
      dodatnaNapomena:inspekcija.dodatnaNapomena,
      radnikID:inspekcija.radnikID,
      prilozenPDF:inspekcija.prilozenPDF,
      pdf:inspekcija.pdf
    }
  }

  updateInspekcija(){
    this.httpKlijent.post(MojConfig.adresa_servera+"/SanitarnaDeratizacija/Update/"+this.odabrana_inspekcija.sanitarnaDeratizacijaID,this.odabrana_inspekcija).subscribe(
      (x:any)=>{
        this.inspectionEdited=true;
        this.promptBg(true);
        this.preuzmi_inspekcije();
      }
    )
  }

  deleteInspekcija(){
    this.httpKlijent.post(MojConfig.adresa_servera+"/SanitarnaDeratizacija/Delete/"+this.odabrana_inspekcija.sanitarnaDeratizacijaID,this.odabrana_inspekcija.sanitarnaDeratizacijaID).subscribe(
      (x:any)=>{
        this.preuzmi_inspekcije();
      }
    )
  }

  formatdate(datum: any) {
    const format = "dd/MM/yyyy";
    const locale = "en-US";
    return formatDate(datum, format, locale);
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

  uploadNewPDF() {
    // @ts-ignore
    var file = document.getElementById("newPDFID").files[0];

    if(file){
      var reader = new FileReader();
      let this2 = this;
      reader.onload = function () {
        // @ts-ignore
        this2.pdfFileBase64 = reader.result.toString();
        this2.novaInspekcijaObj.pdf = this2.pdfFileBase64;
      }
      reader.readAsDataURL(file);
    }
  }

  replacePDF() {
    // @ts-ignore
    var file = document.getElementById("existingPDFID").files[0];

    if(file){
      var reader = new FileReader();
      let this2 = this;
      reader.onload = function () {
        // @ts-ignore
        this2.pdfFileBase64 = reader.result.toString();
        this2.odabrana_inspekcija.pdf = this2.pdfFileBase64;
      }
      reader.readAsDataURL(file);
    }
  }

  scrollUp() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  goToPrev() {
    if(this.currentPage > 1){
      this.currentPage--;
      this.goToPage(this.currentPage);
    }
  }

  goToNext() {
    if(this.currentPage < this.TotalPages()){
      this.currentPage++;
      this.goToPage(this.currentPage);
    }
  }


}
