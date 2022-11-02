import { AbstractControl } from '@angular/forms';

export function NoSpace(control: AbstractControl): { invalidString: boolean } | null {
  if (control.value?.trim()?.length === 0) {
    return { invalidString: true };
  }
  return null;
}
