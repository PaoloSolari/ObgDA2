import { Component } from '@angular/core';
import { Globals } from './utils/globals';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    title = 'ObgApp';
    constructor(){}
    ngOnInit(){}
    get selectTab(){
        return Globals.selectTab;
    }
}
export class TabGroupBasicExample { }