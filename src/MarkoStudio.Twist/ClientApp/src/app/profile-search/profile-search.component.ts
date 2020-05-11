import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ProfileSearchService } from '../services/profile-search-service';
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
      userName: [null, [Validators.required, Validators.pattern('')]]
    });

    this.userNameControl = this.searchProfile.get('userName');
  }

  public getProfileStatistics(): void {

    let userName = this.userNameControl.value;

    this.profileSearchService.searchProfile(userName).subscribe(result => {

      console.log(result);
      this.userChangeEmitter.emit(result);
    }, error => {
      console.error(error);

      this.errorMessage = error.error.message;
    });
  }
}
