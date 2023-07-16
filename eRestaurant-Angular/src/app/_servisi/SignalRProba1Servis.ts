import {Injectable} from "@angular/core";
import * as signalR from "@microsoft/signalr"
import {MojConfig} from "../moj-config";

@Injectable({
  providedIn:"root"
})
export class SignalRProba1Servis{

  public poruka1 : string = "Hello World!";
  public poruka2 : string = "Hello to you too :)";

  otvoriKanalWebSocket(){
      var connection = new signalR.HubConnectionBuilder()
        .withUrl('https://localhost:7129/poruke-hub-putanja')
        .build();

      connection.on('slanje_poruke1',(p:string)=>{
          this.poruka1 = p;
      });

    connection.on('slanje_poruke2',(p:string)=>{
      this.poruka2= p;
    });

      connection.start().then(()=>{
          console.log("Otvoren kanal WS.");
      });
  }

}
