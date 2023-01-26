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

  async getRestaurantById(id) {
    const res = await api.get('/api/restaurants/' + id)

    logger.log('this is the entire res......', res)


    AppState.restaurant = new Restaurant(res.data)
  }

  async getReports(id) {
    const res = await api.get('/api/restaurants/' + id + '/reports')
    logger.log(res.data)
    AppState.reports = res.data
  }


}



export const restaurantsService = new RestaurantsService()
