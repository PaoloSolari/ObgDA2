import { Component, Input, OnInit } from '@angular/core';
import { Globals } from '../../utils/globals';
import { INIT } from '../../utils/routes';

@Component({
    selector: 'app-demand-list',
    templateUrl: './demand-list.component.html',
    styleUrls: ['./demand-list.component.css']
})
export class DemandListComponent implements OnInit {

    public backUrl = `/${INIT}`;

    constructor() { }

    ngOnInit(): void {
        Globals.selectTab = 1;
    }

}
