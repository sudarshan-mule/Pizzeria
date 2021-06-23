import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, OnInit } from '@angular/core';
import { Router, Routes } from '@angular/router';
import { iFood } from '../../models/iModels';

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {

  foods: iFood[];

  constructor(private router: Router, private httpClient: HttpClient) {

  }

  ngOnInit() {
    this.getFoods();
  }

  getFoods() {
    this.httpClient.get('https://localhost:44361//api/Home/Food')
      .subscribe(
        (result: iFood[]) => {
          this.foods = result;
        },
        (error: HttpErrorResponse) => {
          console.log(error);
        });
  }

  goToOrder(food: iFood) {
    this.router.navigate(['/order']);
    sessionStorage.setItem("food", JSON.stringify(food));
  }
}


