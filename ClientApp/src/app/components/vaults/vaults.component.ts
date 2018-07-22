import { Component, OnInit } from '@angular/core';
import { VaultsService } from '../../services/vaults.service';
import { Vault } from '../../models/Vault';
import { AccountService } from '../../services/account.service';
import { User } from '../../models/User';
import { Router } from '@angular/router';
import { ShareService } from '../../services/share.service';

@Component({
  selector: 'app-vaults',
  templateUrl: './vaults.component.html',
  styleUrls: ['./vaults.component.css']
})
export class VaultsComponent implements OnInit {

  private vaults: Vault[];
  private user: User;
  vault = {
    title: ""
  }

  constructor(private _vaultService: VaultsService, private _accountService: AccountService, private _router: Router, private _shareService:ShareService) { }

  ngOnInit() {
    this._accountService.castUser.subscribe(user => {
      if (user) {
        this.user = user;
      } else {
        this._router.navigate(['account'])
      }
    })
    if (this.user) {
      this._vaultService.getVaults(this.user.id);
      this._vaultService.castVaults.subscribe(vaults => {
        if (vaults) {
          this.vaults = vaults;
        }
      })
    }
  }

  createVault() {
    var newVault = this.vault;
    this._vaultService.createVault(newVault);
    this.vault = {
      title: ""
    }
  }
}
