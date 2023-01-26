import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { Restaurant } from '../models/Restaurant.js'

class RestaurantsService {

  async getRestaurants() {
    const res = await api.get('/api/restaurants')
    logger.log(res.data)
    AppState.restaurants = res.data.map(r => new Restaurant(r))
  }

}



export const restaurantsService = new RestaurantsService()
