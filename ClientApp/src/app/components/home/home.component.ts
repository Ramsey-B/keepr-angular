import { Component, OnInit } from '@angular/core';
import { KeepsService } from '../../services/keeps.service';
import { Keep } from '../../models/Keep';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  private keeps: Keep;

  constructor(private _keepsService:KeepsService) { }

  ngOnInit() {
    this._keepsService.getKeeps();
    this._keepsService.castKeeps.subscribe(keeps => {
      if(keeps) {
        this.keeps = keeps;
      }
    })
  }

}
