import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';

import { AppRoutingModule } from './app-routing/app-routing.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { AccountComponent } from './components/account/account.component';
import { AccountService } from './services/account.service';
import { KeepsService } from './services/keeps.service';
import { VaultsComponent } from './components/vaults/vaults.component';
import { VaultsService } from './services/vaults.service';
import { KeepsComponent } from './components/keeps/keeps.component';
import { VaultComponent } from './components/vault/vault.component';
import { ShareService } from './services/share.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    AccountComponent,
    VaultsComponent,
    KeepsComponent,
    VaultComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'account', component: AccountComponent },
      { path: 'vaults', component: VaultsComponent },
      { path: 'vault/:vaultId', component: VaultComponent},
      { path: 'keeps', component: KeepsComponent}
    ])
  ],
  providers: [AccountService, KeepsService, VaultsService, ShareService],
  bootstrap: [AppComponent]
})
export class AppModule { }
