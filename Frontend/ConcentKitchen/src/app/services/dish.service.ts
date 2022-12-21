import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Dish } from '../Dish';


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}


@Injectable({
  providedIn: 'root'
})
export class DishService {

  private apiUrl = "https://localhost:7279/dish";

  constructor(private http:HttpClient) { }

  getDishes(): Observable<Dish[]> {
    return this.http.get<Dish[]>(this.apiUrl);
  }

  deleteDish(dish: Dish): Observable<Dish> {
    const url = `${this.apiUrl}/${dish.dishId}`;

    return this.http.delete<Dish>(url);
  }
}
