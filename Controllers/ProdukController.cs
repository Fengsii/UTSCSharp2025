using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DTO;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Services;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Validators;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdukController : ControllerBase
    {
        private readonly ProdukService _ProdukService;
        private ValidationResult _validation;

        public ProdukController(ProdukService produkService)
        {
            _ProdukService = produkService;
        }



        // GET: api/<WildAnimalsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = _ProdukService.GetListProduk();
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
                var data = _ProdukService.GetListProduk();
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
        [Route("GetProdukById/{Id}")]
        public IActionResult GetProdukById(int id)
        {

            try
            {
                var data = _ProdukService.GetProdukById(id);
                if (data == null)
                {
                    var responseNotFound = new GeneralResponse
                    {
                        StatusCode = "02",
                        Statusdesc = "Produk Not Found",
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



        [HttpPost("InserDataProduk")]
        public async Task<IActionResult> Post([FromForm] ProdukRequestDTO datareq)
        {
            try
            {
                ValidatorRequestProduk request = new ValidatorRequestProduk();
                _validation = request.Validate(datareq);

                if (_validation.IsValid)
                {
                    var data = await _ProdukService.CreateProduk(datareq); // Panggil method async
                    if (data)
                    {
                        var responseSuccess = new GeneralResponse
                        {
                            StatusCode = "01",
                            Statusdesc = "Insert Produk Success",
                            Data = null
                        };

                        return Ok(responseSuccess);
                    }

                    var responseFailed = new GeneralResponse
                    {
                        StatusCode = "02",
                        Statusdesc = "Insert Produk Failed",
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


        [HttpPut("UpdateDataProduk")]
        public async Task<IActionResult> Put(int Id, [FromForm] ProdukRequestDTO datareq)
        {
            try
            {
                ValidatorRequestProduk request = new ValidatorRequestProduk();
                _validation = request.Validate(datareq);

                if (_validation.IsValid)
                {
                    var dataUpdate = await _ProdukService.UpdateProdukAsync(Id, datareq); // Panggil method async
                    if (dataUpdate)
                    {
                        var responseSuccess = new GeneralResponse
                        {
                            StatusCode = "01",
                            Statusdesc = "Update Produk Success",
                            Data = null
                        };

                        return Ok(responseSuccess);
                    }

                    var responseFailed = new GeneralResponse
                    {
                        StatusCode = "02",
                        Statusdesc = "Update Produk Failed",
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









        //// POST api/<WildAnimalsController>
        //[HttpPost("InserDataProduk")]
        //public IActionResult Post(ProdukRequestDTO datareq)
        //{
        //    try
        //    {
        //        ValidatorRequestProduk request = new ValidatorRequestProduk();
        //        _validation = request.Validate(datareq);

        //        if (_validation.IsValid)
        //        {
        //            var data = _ProdukService.CreateProduk(datareq);
        //            if (data)
        //            {
        //                var responseSuccess = new GeneralResponse
        //                {
        //                    StatusCode = "01",
        //                    Statusdesc = "Insert Produk Success",
        //                    Data = null
        //                };

        //                return Ok(responseSuccess);
        //            }

        //            var responseFailed = new GeneralResponse
        //            {
        //                StatusCode = "02",
        //                Statusdesc = "Inser Produk Failed",
        //                Data = null
        //            };


        //            return BadRequest(responseFailed);
        //        }
        //        else
        //        {
        //            var responseFailed = new GeneralResponse
        //            {
        //                StatusCode = "02",
        //                Statusdesc = _validation.ToString(),
        //                Data = null
        //            };

        //            return BadRequest(responseFailed);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var responseFailed = new GeneralResponse
        //        {
        //            StatusCode = "99",
        //            Statusdesc = "Failed | " + ex.Message.ToString(),
        //            Data = null
        //        };

        //        return BadRequest(responseFailed);
        //    }
        //}


        //[HttpPut("UpdateDataProduk")]
        //public IActionResult Put(int Id, ProdukRequestDTO datareq)
        //{
        //    try
        //    {
        //        ValidatorRequestProduk request = new ValidatorRequestProduk();
        //        _validation = request.Validate(datareq);

        //        if (_validation.IsValid)
        //        {
        //            var dataUpdate = _ProdukService.UpdateProduk(Id, datareq);
        //            if (dataUpdate)
        //            {
        //                var responseSuccess = new GeneralResponse
        //                {
        //                    StatusCode = "01",
        //                    Statusdesc = "Update Produk Success",
        //                    Data = null
        //                };

        //                return Ok(responseSuccess);
        //            }

        //            var responseFailed = new GeneralResponse
        //            {
        //                StatusCode = "02",
        //                Statusdesc = "Update Produk Failed",
        //                Data = null
        //            };

        //            return BadRequest(responseFailed);
        //        }
        //        else
        //        {
        //            var responseFailed = new GeneralResponse
        //            {
        //                StatusCode = "02",
        //                Statusdesc = _validation.ToString(),
        //                Data = null
        //            };

        //            return BadRequest(responseFailed);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var responseFailed = new GeneralResponse
        //        {
        //            StatusCode = "99",
        //            Statusdesc = "Failed | " + ex.Message.ToString(),
        //            Data = null
        //        };

        //        return BadRequest(responseFailed);
        //    }
        //}








        // DELETE api/<WildAnimalsController>/5
        //[HttpDelete("{id}")]
        [HttpDelete]
        [Route("DeleteProdukById/{Id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var data = _ProdukService.DeleteProduk(id);
                if (data)
                {
                    var responseSuccess = new GeneralResponse
                    {
                        StatusCode = "01",
                        Statusdesc = "Delete Produk Success",
                        Data = null
                    };

                    return Ok(responseSuccess);
                }

                var responseFailed = new GeneralResponse
                {
                    StatusCode = "02",
                    Statusdesc = "Delete Produk Failed",
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
