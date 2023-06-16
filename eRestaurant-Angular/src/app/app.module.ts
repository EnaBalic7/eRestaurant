import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {RouterModule, RouterOutlet} from "@angular/router";
import {RezervacijeComponent} from "./rezervacije/rezervacije.component";
import {ProizvodiComponent} from "./Proizvodi/proizvodi.component";
import {HttpClientModule} from "@angular/common/http";
import {FormsModule} from "@angular/forms";
import { DatePipe } from '@angular/common'
import {SastojciComponent} from "./Sastojci/sastojci.component";
import {RadniciComponent} from "./Radnici/radnici.component"
import {PitanjaComponent} from "./Pitanja/pitanja.component";
import {UplateComponent} from "./Uplate/uplate.component";
import {KuponiComponent} from "./Kuponi/kuponi.component";
import {FAQComponent} from "./FAQ/faq.component";
import {IzvjestajiComponent} from "./Izvjestaji/izvjestaji.component";
import {InspekcijeComponent} from "./Inspekcije/inspekcije.component";
import {DostaveComponent} from "./Dostave/dostave.component";
import {ChatComponent} from "./Chat/chat.component";


@NgModule({
  declarations: [
    AppComponent,
    RezervacijeComponent,
    ProizvodiComponent,
    SastojciComponent,
    RadniciComponent,
    PitanjaComponent,
    UplateComponent,
    KuponiComponent,
    FAQComponent,
    IzvjestajiComponent,
    InspekcijeComponent,
    DostaveComponent,
    ChatComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot([
      {path: 'putanja-rezervacije', component: RezervacijeComponent},
      {path: 'putanja-proizvodi', component: ProizvodiComponent},
      {path: 'putanja-sastojci', component: SastojciComponent},
      {path: 'putanja-radnici', component: RadniciComponent},
      {path:'putanja-pitanja', component: PitanjaComponent},
      {path:'putanja-uplate', component: UplateComponent},
      {path:'putanja-kuponi', component: KuponiComponent},
      {path:'putanja-faq', component: FAQComponent},
      {path:'putanja-izvjestaji', component: IzvjestajiComponent},
      {path:'putanja-inspekcije', component: InspekcijeComponent},
      {path:'putanja-dostave', component: DostaveComponent},
      {path:'putanja-chat', component: ChatComponent}
      //dodati ovdje komponentu
    ]),
    FormsModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
