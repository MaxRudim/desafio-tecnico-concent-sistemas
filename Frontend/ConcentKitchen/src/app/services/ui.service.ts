import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UiService {

  private showAddDish: boolean = false;

  private subject = new Subject<any>();

  constructor() { }

  toggleAddDish():void {
    this.showAddDish = !this.showAddDish;
    this.subject.next(this.showAddDish);
  }

  onToggle(): Observable<any> {
    return this.subject.asObservable();
  }
}
