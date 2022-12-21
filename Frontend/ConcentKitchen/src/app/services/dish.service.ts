import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Dish } from '../Dish';

@Injectable({
  providedIn: 'root'
})
export class DishService {

  private apiUrl = "https://localhost:7279/dish";

  constructor(private http:HttpClient) { }

  getDishes(): Observable<Dish[]> {
    return this.http.get<Dish[]>(this.apiUrl);
  }
}
