import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toastr = inject(ToastrService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 400) {
        // const messages = error.error?.errors
        //   ? Object.values(error.error.errors).flat()
        //   : [error.error?.message || 'Bad request.'];
        // messages.forEach(msg => toastr.error(msg));
        toastr.error('Bad request.');
      } else if (error.status === 404) {
        toastr.error('Not found.');
      } else if (error.status === 500) {
        toastr.error('Server error. Please try again later.');
      } else {
        toastr.error('Unexpected error.');
      }

      return throwError(() => error);
    })
  );
};
