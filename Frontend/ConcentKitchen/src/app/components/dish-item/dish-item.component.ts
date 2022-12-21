import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Dish } from 'src/app/Dish';
import { faTimes } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-dish-item',
  templateUrl: './dish-item.component.html',
  styleUrls: ['./dish-item.component.css']
})
export class DishItemComponent implements OnInit {

  @Input()
  dish!: Dish;

  @Output() onDeleteDish: EventEmitter<Dish> = new
  EventEmitter()

  faTimes = faTimes;

  constructor() {}

  ngOnInit(): void {

  }

  onDelete(dish: any) {
    this.onDeleteDish.emit(dish);
  }

}
