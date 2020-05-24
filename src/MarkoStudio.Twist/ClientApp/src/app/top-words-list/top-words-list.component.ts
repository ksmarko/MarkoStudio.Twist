import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-top-words-list',
  templateUrl: './top-words-list.component.html',
  styleUrls: ['./top-words-list.component.scss']
})
export class TopWordsListComponent {

  @Input() profileTopWords: TopWordListItem[];
}

export class TopWordListItem {
  text: string;
  value: number;
}
