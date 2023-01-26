import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"

class ReportsService {

  async createReport(reportData) {
    const res = await api.post('/api/reports', reportData)
    let report = res.data
    AppState.reports.push(report)
  }
}

export const reportsService = new ReportsService()
