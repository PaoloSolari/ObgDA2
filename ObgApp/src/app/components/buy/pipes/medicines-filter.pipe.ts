import { Pipe, PipeTransform } from '@angular/core';
import { Medicine } from '../../../models/medicine';

@Pipe({
  name: 'medicinesFilter'
})
export class MedicinesFilterPipe implements PipeTransform {

  transform(medicines: Medicine[] | undefined, filterValue: string): Medicine[] {
    // 5) Escribimos el código para filtrar las películas
    // El primer parámetro (movies) es la lista de películas que vamos a transformar
    // El segundo parámetro (filterValue) es el criterio que vamos a utilizar para transformar
    // en este caso vamos a estar filtrando las películas por ese filterValue
    // El retorno es la lista de películas filtradas por filterValue
    if(!medicines) {
      return [];
    }
    return medicines.filter((medicine) => {
        
        // medicine.name.toLowerCase().includes(filterValue.toLowerCase());

    })
  }

}
