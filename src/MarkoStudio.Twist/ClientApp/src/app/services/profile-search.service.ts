import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProfileStatistics } from '../models/profile-statistics.model';
import { Observable } from 'rxjs';

@Injectable()
export class ProfileSearchService {
    private baseUrl: string;

    constructor(
        private httpClient: HttpClient,
        @Inject('BASE_URL') baseUrl: string
    ) {
        this.baseUrl = baseUrl;
    }

    public searchProfile(userName: string) : Observable<ProfileStatistics> {
    
        var url = this.baseUrl + `api/statistics/profile?userName=${userName}`;
    
        return this.httpClient.get<ProfileStatistics>(url);
    }
}