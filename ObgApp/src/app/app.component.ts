import { Component } from '@angular/core';
import { Globals } from './utils/globals';
import { USER_FORM_URL } from './utils/routes';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    title = 'ObgApp';

    public addUser = `/${USER_FORM_URL}`;

    constructor() { }
    ngOnInit() { }
    get selectTab() {
        return Globals.selectTab;
    }
}
export class TabGroupBasicExample { }