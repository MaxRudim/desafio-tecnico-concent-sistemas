import { Component, OnInit, Input } from '@angular/core';
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

  faTimes = faTimes;

  constructor() {}

  ngOnInit(): void {

  }

}
