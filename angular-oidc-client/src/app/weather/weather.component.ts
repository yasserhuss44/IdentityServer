import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from '../LocalStorage.service';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.scss'],
})
export class WeatherComponent implements OnInit {
  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.

    const httpOptions = {
      headers: {
        'Content-Type': 'application/json',
        authorization: `Bearer ${
          this.localStorage.getItem('loginResponse').idToken
        }`,
      },
    };

    this.httService
      .get('https://localhost:6001/WeatherForecast', httpOptions)
      .subscribe((r) => console.log(r));
  }
  constructor(
    private httService: HttpClient,
    private localStorage: LocalStorageService
  ) {}
}
