using Microsoft.AspNetCore.Mvc;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DTO;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Services;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Validators;
using FluentValidation.Results;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PesananController : ControllerBase
    {

        private readonly PesananService _PesananService;
        private ValidationResult _validation;

        public PesananController(PesananService pesananService)
        {
            _PesananService = pesananService;
        }



        // GET: api/<WildAnimalsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = _PesananService.GetListPesanan();
                var response = new GeneralResponse
                {
                    StatusCode = "01",
                    Statusdesc = "Sukses",
                    Data = data
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var data = _PesananService.GetListPesanan();
                var response = new GeneralResponse
                {
                    StatusCode = "99",
                    Statusdesc = "Failed | " + ex.Message.ToString(),
                    Data = null
                };
                return BadRequest(response);
            }
        }

        // GET api/<WildAnimalsController>/5
        //[HttpGet("{id}")]
        [HttpGet]
        [Route("GetPesananById/{Id}")]
        public IActionResult GetPesananById(int id)
        {

            try
            {
                var data = _PesananService.GetPesananById(id);
                if (data == null)
                {
                    var responseNotFound = new GeneralResponse
                    {
                        StatusCode = "02",
                        Statusdesc = "Pesanan Not Found",
                        Data = null
                    };
                    return NotFound(responseNotFound);
                }

                var responseSuccess = new GeneralResponse
                {
                    StatusCode = "01",
                    Statusdesc = "Success",
                    Data = data
                };
                return Ok(responseSuccess);
            }
            catch (Exception ex)
            {
                var responseFailed = new GeneralResponse
                {
                    StatusCode = "99",
                    Statusdesc = "Failed | " + ex.Message.ToString(),
                    Data = null
                };
                return BadRequest(responseFailed);
            }
        }

        // POST api/<WildAnimalsController>
        [HttpPost("InserDataPesanan")]
        public IActionResult Post(PesananRequestDTO datareq)
        {
            try
            {
                ValidatorRequestPesanan request = new ValidatorRequestPesanan();
                _validation = request.Validate(datareq);

                if (_validation.IsValid)
                {
                    // Cek apakah produk yang dipesan ada dalam database
                    if (!_PesananService.CheckIDProduk(datareq.IdProduk))
                    {
                        var responProdukTidakAda = new GeneralResponse
                        {
                            StatusCode = "02",
                            Statusdesc = "Produk yang dipesan tidak tersedia dalam database.",
                            Data = null
                        };

                        return BadRequest(responProdukTidakAda);
                    }


                    var data = _PesananService.CreatePesanan(datareq);
                    if (data)
                    {
                        var responseSuccess = new GeneralResponse
                        {
                            StatusCode = "01",
                            Statusdesc = "Insert Pesanan Success",
                            Data = null
                        };

                        return Ok(responseSuccess);
                    }

                    var responseFailed = new GeneralResponse
                    {
                        StatusCode = "02",
                        Statusdesc = "Inser Pesanan Failed",
                        Data = null
                    };


                    return BadRequest(responseFailed);
                }
                else
                {
                    var responseFailed = new GeneralResponse
                    {
                        StatusCode = "02",
                        Statusdesc = _validation.ToString(),
                        Data = null
                    };

                    return BadRequest(responseFailed);

                }
            }
            catch (Exception ex)
            {
                var responseFailed = new GeneralResponse
                {
                    StatusCode = "99",
                    Statusdesc = "Failed | " + ex.Message.ToString(),
                    Data = null
                };

                return BadRequest(responseFailed);
            }
        }


        [HttpPut("UpdateDataPesanan")]
        public IActionResult Put(int Id, PesananRequestDTO datareq)
        {
            try
            {
                ValidatorRequestPesanan request = new ValidatorRequestPesanan();
                _validation = request.Validate(datareq);

                if (_validation.IsValid)
                {
                    // Cek apakah produk yang dipesan ada dalam database
                    if (!_PesananService.CheckIDProduk(datareq.IdProduk))
                    {
                        var responProdukTidakAda = new GeneralResponse
                        {
                            StatusCode = "02",
                            Statusdesc = "Produk yang dipesan tidak tersedia dalam database.",
                            Data = null
                        };

                        return BadRequest(responProdukTidakAda);
                    }




                    var dataUpdate = _PesananService.UpdatePesanan(Id, datareq);
                    if (dataUpdate)
                    {
                        var responseSuccess = new GeneralResponse
                        {
                            StatusCode = "01",
                            Statusdesc = "Update Pesanan Success",
                            Data = null
                        };

                        return Ok(responseSuccess);
                    }

                    var responseFailed = new GeneralResponse
                    {
                        StatusCode = "02",
                        Statusdesc = "Update Pesanan Failed",
                        Data = null
                    };

                    return BadRequest(responseFailed);
                }
                else
                {
                    var responseFailed = new GeneralResponse
                    {
                        StatusCode = "02",
                        Statusdesc = _validation.ToString(),
                        Data = null
                    };

                    return BadRequest(responseFailed);
                }
            }
            catch (Exception ex)
            {
                var responseFailed = new GeneralResponse
                {
                    StatusCode = "99",
                    Statusdesc = "Failed | " + ex.Message.ToString(),
                    Data = null
                };

                return BadRequest(responseFailed);
            }
        }








        // DELETE api/<WildAnimalsController>/5
        //[HttpDelete("{id}")]
        [HttpDelete]
        [Route("DeletePesananById/{Id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var data = _PesananService.DeletePesanan(id);
                if (data)
                {
                    var responseSuccess = new GeneralResponse
                    {
                        StatusCode = "01",
                        Statusdesc = "Delete Pesanan Success",
                        Data = null
                    };

                    return Ok(responseSuccess);
                }

                var responseFailed = new GeneralResponse
                {
                    StatusCode = "02",
                    Statusdesc = "Delete Pesanan Failed",
                    Data = null
                };

                return BadRequest(responseFailed);
            }
            catch (Exception ex)
            {
                var responseFailed = new GeneralResponse
                {
                    StatusCode = "99",
                    Statusdesc = "Failed | " + ex.Message.ToString(),
                    Data = null
                };

                return BadRequest(responseFailed);
            }
        }

    }
}
