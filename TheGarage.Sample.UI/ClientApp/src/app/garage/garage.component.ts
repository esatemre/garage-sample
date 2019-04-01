import { Component, ViewEncapsulation } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment'
import { GarageOverview } from '../_models'

@Component({
  selector: 'app-garage',
  templateUrl: './garage.component.html'
})
export class GarageComponent {
  public myGarage: GarageOverview;

  constructor(private httpClient: HttpClient) {
    this.getDetail();
  }

  getDetail() {
    this.httpClient.get<GarageOverview>(environment.apiUrl + '/Garage/GetDetail').subscribe(result => {
      this.myGarage = result;
    }, error => console.error(error));
  }

  refresh() {
    this.httpClient.get<GarageOverview>(environment.apiUrl + '/Garage/Refresh').subscribe(result => {
      this.myGarage = result;
    }, error => console.error(error));
  }

  collapseHistory(event, ele) {
    event.target.innerText = (event.target.innerText == 'Hide' ? 'Show' : 'Hide');
    ele.style.display = (ele.style.display == 'none' ? '' : 'none');
  }
}
