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

  async createRestaurant(restaurantData) {
    const res = await api.post('/api/restaurants', restaurantData)
    logger.log('wut', res.data)
    AppState.restaurants.push(new Restaurant(res.data))
  }

  async getReports(id) {
    const res = await api.get('/api/restaurants/' + id + '/reports')
    logger.log(res.data)
    AppState.reports = res.data
  }

  async shutItDown(id) {
    await api.put('/api/restaurants/' + id, { ...AppState.restaurant, shutdown: true })
    AppState.restaurant.shutdown = true
  }

  async reopen(id) {
    await api.put('/api/restaurants/' + id, { ...AppState.restaurant, shutdown: false })
    AppState.restaurant.shutdown = false
  }


}



export const restaurantsService = new RestaurantsService()
