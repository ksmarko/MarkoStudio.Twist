import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ProfileSearchService } from '../services/profile-search.service';
import { FormBuilder, Validators, AbstractControl, FormGroup } from '@angular/forms';
import { ProfileStatistics } from '../models/profile-statistics.model';

@Component({
  selector: 'app-profile-search',
  templateUrl: './profile-search.component.html',
  styleUrls: ['./profile-search.component.scss']
})
export class ProfileSearchComponent implements OnInit {

  @Output('userChange') userChangeEmitter: EventEmitter<ProfileStatistics> = new EventEmitter<ProfileStatistics>();

  private userNameControl: AbstractControl;
  public searchProfile: FormGroup;

  public errorMessage: string;

  constructor(
    private profileSearchService: ProfileSearchService,
    private formBuilder: FormBuilder
  ) { }

  public ngOnInit(): void {
    this.searchProfile = this.formBuilder.group({
      userName: [null, [Validators.required, Validators.pattern('[^\w]'), Validators.nullValidator]] // todo: fix
    });

    this.userNameControl = this.searchProfile.get('userName');

    this.userNameControl.setValue('realDonaldTrump');
    this.getProfileStatistics();
  }

  public getProfileStatistics(): void {

    let userName = this.userNameControl.value;

    var profile = localStorage.getItem(userName);

    if (profile) {
      var result = JSON.parse(profile);

      this.errorMessage = null;
      console.log(`From cache: ${result}`);
      this.userChangeEmitter.emit(result);
      return;
    }

    this.profileSearchService.searchProfile(userName).subscribe(result => {

      localStorage.setItem(userName, JSON.stringify(result));

      this.errorMessage = null;
      console.log(result);
      console.log(`From real services: ${result}`);
      this.userChangeEmitter.emit(result);
    }, error => {
      console.error(error);
      this.errorMessage = error.error.message;
      this.userChangeEmitter.emit(null);
    });
  }
}
