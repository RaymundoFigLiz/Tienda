export interface Wrapper<T> {
  statusCode: boolean;
  message: string;
  result: T;
}
