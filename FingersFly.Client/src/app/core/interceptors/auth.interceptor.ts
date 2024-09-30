import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const cloneRed = req.clone({
    withCredentials: true
  })

  return next(cloneRed);
};
