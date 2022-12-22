import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { UiService } from 'src/app/services/ui.service';
import { Subscription } from 'rxjs';
import { Dish } from 'src/app/Dish';

@Component({
  selector: 'app-add-dish',
  templateUrl: './add-dish.component.html',
  styleUrls: ['./add-dish.component.css']
})
export class AddDishComponent implements OnInit {

  @Output() onAddDish: EventEmitter<Dish> = new EventEmitter();

  name!: string;
  price!: number;
  preparation!: number;
  ingredients!: string;
  category!: string;
  showAddDish!: boolean;
  subscription!: Subscription;

  constructor(private uiService: UiService) {
    this.subscription = this.uiService
    .onToggle()
    .subscribe(value => this.showAddDish = value);
  }

  ngOnInit(): void {

  }

  onSubmit() {
    if (!this.name || !this.price || !this.preparation || !this.ingredients || !this.category) {
      alert('Por favor, preencha todos os campos para adicionar um prato.');
      return;
    }

    const newDish = {
      dishName: this.name,
      dishPrice: this.price,
      dishPreparationTimeInMinutes: this.preparation,
      dishIngredients: this.ingredients,
      dishCategory: this.category,
    }

    this.onAddDish.emit(newDish);

    this.name = '';
    this.price = 0;
    this.preparation = 0;
    this.ingredients = '';
    this.category = '';
  }
}
