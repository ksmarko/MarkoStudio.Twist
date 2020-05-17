import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { ReactiveFormsModule } from '@angular/forms';

import { ChartsModule } from 'ng2-charts';
import { ProfileSearchComponent } from './profile-search/profile-search.component';
import { ProfileSearchService } from './services/profile-search.service';
import { ProfileSentimentPieChartComponent } from './profile-sentiment-pie-chart/profile-sentiment-pie-chart.component';
import { ProfileSummaryComponent } from './profile-summary/profile-summary.component';
import { TweetsDetailsComponent } from './tweets-details/tweets-details.component';
import { ProfileToxicityPieChartComponent } from './profile-toxicity-pie-chart/profile-toxicity-pie-chart.component';
import { ProfileAggregateService } from './services/profile-statistics-aggregate.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProfileSearchComponent,
    ProfileSentimentPieChartComponent,
    ProfileSummaryComponent,
    TweetsDetailsComponent,
    ProfileToxicityPieChartComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ChartsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' }
    ])
  ],
  providers: [ProfileSearchService, ProfileAggregateService],
  bootstrap: [AppComponent]
})
export class AppModule { }
