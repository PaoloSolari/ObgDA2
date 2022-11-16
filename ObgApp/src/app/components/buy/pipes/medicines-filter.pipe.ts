import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'medicinesFilter'
})
export class MedicinesFilterPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    return null;
  }

}
