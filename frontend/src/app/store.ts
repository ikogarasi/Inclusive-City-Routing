import { Action, ThunkAction, configureStore } from "@reduxjs/toolkit";
import externalServicesRktApi from "../api/externalServicesRktApi";

export const store = configureStore({
  reducer: {
    [externalServicesRktApi.reviewRtkApi.reducerPath]:
      externalServicesRktApi.reviewRtkApi.reducer,
    [externalServicesRktApi.structureRtkApi.reducerPath]:
      externalServicesRktApi.structureRtkApi.reducer,
    [externalServicesRktApi.routingRtkApi.reducerPath]:
      externalServicesRktApi.routingRtkApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware()
      .concat(externalServicesRktApi.reviewRtkApi.middleware)
      .concat(externalServicesRktApi.structureRtkApi.middleware)
      .concat(externalServicesRktApi.routingRtkApi.middleware),
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
