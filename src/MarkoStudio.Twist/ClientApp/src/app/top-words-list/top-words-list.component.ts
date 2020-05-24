import { Component, Input } from '@angular/core';
import { TopWordListItem } from '../models/profile-statistics.model';

@Component({
  selector: 'app-top-words-list',
  templateUrl: './top-words-list.component.html',
  styleUrls: ['./top-words-list.component.scss']
})
export class TopWordsListComponent {

  @Input() profileTopWords: TopWordListItem[];
}
