export type User = {
  username: string;
  displayName: string;
  token: string;
  image?: string;
};

export type UserFormValues = {
  email: string;
  password: string;
  displayName?: string;
  username?: string;
};
