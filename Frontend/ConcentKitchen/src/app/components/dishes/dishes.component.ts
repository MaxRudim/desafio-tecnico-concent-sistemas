import { Component, OnInit } from '@angular/core';
import { DishService } from 'src/app/services/dish.service';
import { Dish } from 'src/app/Dish';

@Component({
  selector: 'app-dishes',
  templateUrl: './dishes.component.html',
  styleUrls: ['./dishes.component.css']
})
export class DishesComponent implements OnInit {
  dishes: Dish[] = [];

  constructor(private dishService: DishService) { }

  ngOnInit(): void {
    this.dishService.getDishes().subscribe((dishes) => this.dishes = dishes);
  }

  deleteDish(dish: Dish) {
    this.dishService
      .deleteDish(dish)
      .subscribe(() => (this.dishes = this.dishes.filter(t => t.dishId !== dish.dishId)));
  }
}
