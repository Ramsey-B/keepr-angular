import { Component, OnInit } from '@angular/core';
import { ShareService } from '../../services/share.service';
import { ActivatedRoute } from '@angular/router';
import { Keep } from '../../models/Keep';

@Component({
  selector: 'app-vault',
  templateUrl: './vault.component.html',
  styleUrls: ['./vault.component.css']
})
export class VaultComponent implements OnInit {
  vaultId:number;
  keeps:Keep[];

  constructor(private _shareService:ShareService, private route:ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.vaultId = params["vaultId"];
    })
    if(this.vaultId) {
      this._shareService.getSharesByVaultId(this.vaultId);
    }
    this._shareService.keeps.subscribe(keeps => {
      this.keeps = keeps;
    })
  }

}
