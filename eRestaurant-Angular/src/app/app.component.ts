import { Component,  ElementRef, Renderer2 } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'eRestaurant-Angular';
  storageIconSrc: any ="assets\\Resources\\storage.svg";
  FAQIconSrc: any = "assets\\Resources\\question.svg";
  FAQIconSrc2: any = "assets\\Resources\\question.svg";
  employeesIconSrc: any = "assets\\Resources\\employees.svg";
  salesReportsIconSrc:any="assets\\Resources\\salesReports.svg";
  inspectionsIconSrc:any="assets\\Resources\\inspections.svg";
  deliveriesIconSrc:any="assets\\Resources\\deliveries.svg"
  chatIconSrc:any="assets\\Resources\\chat.svg";

  constructor(private router: Router, private route: ActivatedRoute, private renderer: Renderer2, private elementRef: ElementRef) {
  }

  isNavItemActive(navItemLink: string): boolean {
    return this.router.isActive(navItemLink, false);
  }

  changeImageSource(isActive: boolean, id: string): void {
    const imageElement = this.elementRef.nativeElement.querySelector('#'+id);
    if (isActive) {
      this.renderer.setAttribute(imageElement, 'src', 'assets/Resources/'+id+'_hover.svg');
    }
    else {
      this.renderer.setAttribute(imageElement, 'src', 'assets/Resources/'+id+'.svg');
    }
  }

}
