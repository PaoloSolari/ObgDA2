import { Component, OnInit } from '@angular/core';
import { Globals } from 'src/app/utils/globals';

@Component({
    selector: 'app-init',
    templateUrl: './init.component.html',
    styleUrls: ['./init.component.css']
})
export class InitComponent implements OnInit {

    public selectTab: number = Globals.selectTab;

    constructor() { }

    ngOnInit(): void {
    }

}
