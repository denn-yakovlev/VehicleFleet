import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { ChartsModule, ThemeService } from 'ng2-charts'

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { DriversComponent } from "./drivers/drivers.component";
import { ShiftsComponent } from "./shifts/shifts.component";
import { VehiclesComponent } from "./vehicles/vehicles.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    DriversComponent,
    ShiftsComponent,
    VehiclesComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgbModule,
    ChartsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'drivers', component: DriversComponent },
      { path: 'shifts', component: ShiftsComponent },
      { path: 'vehicles', component: VehiclesComponent },
    ])
  ],
  providers: [ThemeService],
  bootstrap: [AppComponent]
})
export class AppModule { }
