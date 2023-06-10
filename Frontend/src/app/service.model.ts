export interface GenericResult 
{
  error: GenericError;
  result: any;
  success: boolean 
}

export interface GenericError { 
    code: number;
    details: string;
    message: string;
    validationErrors: any;
}