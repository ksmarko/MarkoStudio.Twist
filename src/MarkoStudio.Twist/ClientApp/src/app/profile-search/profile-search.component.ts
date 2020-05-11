import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ProfileSearchService } from '../services/profile-search-service';
import { FormBuilder, Validators, AbstractControl, FormGroup } from '@angular/forms';
import { ProfileStatistics } from '../models/profile-statistics.model';

@Component({
  selector: 'app-profile-search',
  templateUrl: './profile-search.component.html',
  styleUrls: ['./profile-search.component.css']
})
export class ProfileSearchComponent implements OnInit {

  @Output('userChange') userChangeEmitter: EventEmitter<ProfileStatistics> = new EventEmitter<ProfileStatistics>();

  private emailControl: AbstractControl;
  public myGroup: FormGroup;

  public errorMessage: string;

  constructor(
    private profileSearchService: ProfileSearchService,
    private formBuilder: FormBuilder
  ) { }

  public ngOnInit(): void {
    this.myGroup = this.formBuilder.group({
      email: [null, [Validators.required, Validators.pattern('')]]
    });

    this.emailControl = this.myGroup.get('email');
  }

  public getProfileStatistics(): void {

    let userName = this.emailControl.value;

    this.profileSearchService.searchProfile(userName).subscribe(result => {

      console.log(result);
      this.userChangeEmitter.emit(result);
    }, error => {
      console.error(error);

      this.errorMessage = error.error.message;
    });
  }
}
