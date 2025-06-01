import {
  ReviewClient as OverpassReviewClient,
  StructureClient as OverpassStructureClient,
  RoutingClient,
} from "./api/externalServicesApi";

const httpURL1 = "https://localhost:7171";

export const overpassReviewClient = new OverpassReviewClient(httpURL1);
export const overpassStructureClient = new OverpassStructureClient(httpURL1);
export const osrmClient = new RoutingClient(httpURL1);
