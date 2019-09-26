import { Component } from '@angular/core';
import { ApiService } from './api.service';
import { WorkItem } from './model/work-item';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
  
  title = 'FrontEngenhosDevops';
  displayedColumns: string[] = [ 'nome', 'desc', 'preco', 'acao'];
  dataSource: WorkItem[];

  constructor(private _api: ApiService){}

  ngOnInit() {
    this._api.getWorkItens()
    .subscribe(res => {
      this.dataSource = res;
      console.log(this.dataSource);
    }, err => {
      console.log(err);
    });
  }

  public key: string = 'WorkItemId';
  reverse: boolean = false;
  sort(key) {
      this.key = key;
      this.reverse = !this.reverse;
  }

}
