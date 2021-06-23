import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit } from '@angular/core';
import { iFood, iOrder, iIngredients, iToppings } from '../../models/iModels';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})

export class OrderComponent implements OnInit, AfterViewInit, OnDestroy {

  currentTab = 0; // Current tab is set to be the first tab (0)
  order: iOrder = {};
  pizzaSize: string[] = [];
  pizzaIngredients: iIngredients[] = [];
  splittedPizzaIngredients: any[] = [];
  pizzaToppings: iToppings[] = [];
  splittedPizzaToppings: any[] = [];
  orderId: number = 0;
  showSuccessOrder: boolean = false;
  errorMessage: string;
  isDisabled: boolean = false;

  constructor(private httpClient: HttpClient) {
    this.order.food = <iFood>JSON.parse(sessionStorage.getItem("food"));
    this.order.size = 'Small';
    this.order.quantity = 1;
    this.order.ingredients = [];
    this.order.toppings = [];
    this.order.grandTotal = this.order.food.price;
  }

  ngOnInit() {
    this.getMasterData();
    this.showTab(this.currentTab); // Display the current tab
  }

  ngAfterViewInit() {

  }

  getMasterData() {
    this.getSize();
    this.getIngredients();
    this.getToppings();
  }

  getSize() {
    this.httpClient.get('https://localhost:44361//api/Order/Size')
      .subscribe(
        (result: string[]) => {
          this.pizzaSize = result;
        },
        (error: HttpErrorResponse) => {
          console.log(error);
        });
  }

  getIngredients() {
    this.httpClient.get('https://localhost:44361//api/Order/Ingredients')
      .subscribe(
        (result: iIngredients[]) => {
          this.pizzaIngredients = result;
          this.splittedPizzaIngredients = this.splitArray(this.pizzaIngredients, 3);
        },
        (error: HttpErrorResponse) => {
          console.log(error);
        });
  }

  getToppings() {
    this.httpClient.get('https://localhost:44361//api/Order/Toppings')
      .subscribe(
        (result: iToppings[]) => {
          this.pizzaToppings = result;
          this.splittedPizzaToppings = this.splitArray(this.pizzaToppings, 3);
        },
        (error: HttpErrorResponse) => {
          console.log(error);
        });
  }

  splitArray(array: any[], chunk: number): any[] {
    let i, j, temp;
    let finalArray: any[] = [];
    for (i = 0, j = array.length; i < j; i += chunk) {
      temp = array.slice(i, i + chunk);
      finalArray.push(temp);
    }
    return finalArray;
  }

  showTab(n) {
    // This function will display the specified tab of the form...
    var x: any = document.getElementsByClassName("tab");
    x[n].style.display = "block";
    //... and fix the Previous/Next buttons:
    if (n == 0) {
      document.getElementById("prevBtn").style.display = "none";
    } else {
      document.getElementById("prevBtn").style.display = "inline";
    }
    if (n == (x.length - 1)) {
      document.getElementById("nextBtn").innerHTML = "Submit";
    } else {
      document.getElementById("nextBtn").innerHTML = "Next";
    }
    //... and run a function that will display the correct step indicator:
    this.fixStepIndicator(n)
  }

  nextPrev(n) {
    // This function will figure out which tab to display
    var x: any = document.getElementsByClassName("tab");
    // Exit the function if any field in the current tab is invalid:
    if (n == 1 && !this.validateForm()) return false;
    // Hide the current tab:
    x[this.currentTab].style.display = "none";
    // Increase or decrease the current tab by 1:
    this.currentTab = this.currentTab + n;
    // if you have reached the end of the form...
    if (this.currentTab >= x.length) {
      document.getElementById("prevBtn").style.display = "none";
      document.getElementById("nextBtn").style.display = "none";
      //API Call
      this.httpClient.post('https://localhost:44361//api/Order/Create', this.order)
        .subscribe(
          (orderId: number) => {
            this.orderId = orderId;
          },
          (error: HttpErrorResponse) => {
            console.log(error);
            this.errorMessage = error.error.exceptionMessage;
            document.getElementById("prevBtn").style.display = "inline";
            document.getElementById("nextBtn").style.display = "inline";
          });
      return false;
    }
    // Otherwise, display the correct tab:
    this.showTab(this.currentTab);
  }

  validateForm() {
    // This function deals with validation of the form fields
    switch (this.currentTab) {
      case 0: {
        this.isDisabled = this.order.quantity <= 0 ? true : false;
        break;
      }
      case 1: {
        this.isDisabled = (!this.order.size || this.order.size == "") ? true : false;
        break;
      }
      case 2: {
        this.isDisabled = (!this.order.ingredients || this.order.ingredients.length <= 0) ? true : false;
        break;
      }
      case 3: {
        this.isDisabled = (!this.order.toppings || this.order.toppings.length <= 0) ? true : false;
        break;
      }
    }
    return !this.isDisabled; // return the valid status
  }

  fixStepIndicator(n) {
    // This function removes the "active" class of all steps...
    var i, x = document.getElementsByClassName("step");
    for (i = 0; i < x.length; i++) {
      x[i].className = x[i].className.replace(" active", "");
    }
    //... and adds the "active" class on the current step:
    x[n].className += " active";
  }

  incDec(incDec: number) {
    if (incDec == +1) {
      this.order.quantity = this.order.quantity + 1;
    } else if (incDec == -1) {
      let qty = this.order.quantity - 1;
      this.order.quantity = qty > 0 ? qty : 1;
    }
    console.log(this.order);
    this.calculateTotalAmount();
  }

  changeIngredients(ingredient: iIngredients) {
    let isFound: boolean = this.order.ingredients.some(x => x.name == ingredient.name);
    if (ingredient.isChecked && !isFound) {
      this.order.ingredients.push(ingredient)
    } else if (!ingredient.isChecked && isFound) {
      this.order.ingredients = this.order.ingredients.filter(x => x.name != ingredient.name);
    }
    if (this.order.ingredients && this.order.ingredients.length > 0) {
      this.isDisabled = false;
    } else {
      this.isDisabled = true;
    }
    this.calculateTotalAmount()
  };

  changeToppings(topping: iToppings) {
    let isFound: boolean = this.order.toppings.some(x => x.name == topping.name);
    if (topping.isChecked && !isFound) {
      this.order.toppings.push(topping)
    } else if (!topping.isChecked && isFound) {
      this.order.toppings = this.order.toppings.filter(x => x.name != topping.name);
    }
    if (this.order.toppings && this.order.toppings.length > 0) {
      this.isDisabled = false;
    } else {
      this.isDisabled = true;
    }
    this.calculateTotalAmount();
  }

  changeSize() {
    this.calculateTotalAmount();
  }

  calculateTotalAmount() {
    let sizePrice = this.getPriceBySize(this.order.size, this.order.food.price);
    let ingredientsTotal: number = this.order.ingredients.reduce(function (acc, obj) { return acc + obj.price; }, 0);
    let toppingsTotal: number = this.order.toppings.reduce(function (acc, obj) { return acc + obj.price; }, 0);
    this.order.grandTotal = this.order.quantity * (sizePrice  + ingredientsTotal + toppingsTotal);
  }

  getPriceBySize(size: string, price: number): number {
    switch (size.toLowerCase()) {
      case "small": return price * 1;
      case "medium": return price * 1.5;
      case "large": return price * 2;
      default: return price;
    }
  }

  ngOnDestroy() {
    sessionStorage.removeItem("food");
  }
}
