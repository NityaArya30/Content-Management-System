using Microsoft.EntityFrameworkCore;
using SignageBackend.BAL.Infrastructure;
using SignageBackend.BAL.ViewModels;
using SignageBackend.Common.Utilites;
using SignageBackend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SignageBackend.BAL.Services
{
    public class ScheduleServices : ISchedule
    {
        private WSignageContext _context;

        public ScheduleServices(WSignageContext context)
        {
            _context = context;
        }

        public ApiResponse CreateOrUpdate(ScheduleView scheduleView)
        {
            ApiResponse res = new ApiResponse();
            if (scheduleView.ScheduleId != null)
            {

                var querydata = (from schedule in _context.Schedules
                                 where schedule.CreatedBy == scheduleView.CreatedBy //&& schedule.Priority == 3 
                                 && (schedule.StartTime <= scheduleView.StartTime &&
                                 schedule.EndTime >= scheduleView.StartTime) ||
                               (schedule.StartTime <= scheduleView.EndTime &&
                               schedule.EndTime >= scheduleView.EndTime)
                                 select schedule).ToList();
                if (querydata.Count() > 0)
                {
                    res.Status = "error";
                    res.Code = 200;
                    res.Message = "Please select different date and time. A schedule already exists.";
                    return res;
                }


                if (scheduleView.ScheduleId != 0)
                {
                    Schedule a = _context.Schedules.Where(x => x.ScheduleId == scheduleView.ScheduleId).FirstOrDefault();
                    if (a != null)
                    {
                        a.ScheduleId = scheduleView.ScheduleId;
                        a.LayoutId = scheduleView.LayoutId;
                        a.StartTime = scheduleView.StartTime;
                        a.EndTime = scheduleView.EndTime;
                        a.Priority = scheduleView.Priority;
                        a.CreatedBy = scheduleView.CreatedBy;
                        a.Recurrence = scheduleView.Recurrence;
                        a.CreatedAt = scheduleView.CreatedAt;
                        a.UpdatedAt = DateTime.Now;

                        _context.Schedules.Update(a);
                        _context.SaveChanges();
                        res.Status = "success";
                        res.Code = 200;
                        res.Message = "Schedule Updated Successfully.";
                    }
                }
                else
                {
                    Schedule a = new Schedule();
                    a.ScheduleId = scheduleView.ScheduleId;
                    a.LayoutId = scheduleView.LayoutId;
                    a.StartTime = scheduleView.StartTime;
                    a.EndTime = scheduleView.EndTime;
                    a.Priority = scheduleView.Priority;
                    a.CreatedBy = scheduleView.CreatedBy;
                    a.Recurrence = scheduleView.Recurrence;
                    a.CreatedAt = DateTime.Now;
                    a.UpdatedAt = null;

                    _context.Schedules.Add(a);
                    _context.SaveChanges();
                    res.Status = "success";
                    res.Code = 200;
                    res.Message = "Schedule Added Successfully.";
                }

            }
            else
            {
                res.Status = "error";
                res.Code = 200;
                res.Message = "Please enter valid data.";
            }
            return res;
        }

        public async Task<PaginatedResult<ScheduleView>> GetAll(
                string? searchTerm = null,     // Optional search term
                string sortBy = "Name",       // Default sort by "Name"
                bool sortDescending = false,  // Default ascending order
                int pageNumber = 1,           // Default page number
                int pageSize = 10             // Default page size
            )
        {
            // Ensure page number and size are valid
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;
            //List<(int, string)> priorityvalue = new List<(int, string)> { (1, "Low"), (2, "Medium"), (3, "High") };

            var query = from a in _context.Schedules
                        join layout in _context.Layouts
                        on a.LayoutId equals layout.LayoutId
                        //join pv in priorityvalue 
                        //on a.Priority  equals pv.Item1
                        select new ScheduleView
                        {
                            ScheduleId = a.ScheduleId,
                            LayoutId = a.LayoutId,
                            LayoutName = layout.Name,
                            StartTime = a.StartTime,
                            EndTime = a.EndTime,
                            Priority = a.Priority,
                            PriorityValue = a.Priority == 3 ? "High" : a.Priority == 2 ? "Medium" : " Low",
                            CreatedBy = a.CreatedBy,
                            Recurrence = a.Recurrence,
                            UpdatedAt = a.UpdatedAt,
                            CreatedAt = a.CreatedAt
                        };

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(x => x.Recurrence.Contains(searchTerm));
            }

            // Apply sorting
            query = sortBy.ToLower() switch
            {
                "name" => sortDescending ? query.OrderByDescending(x => x.Recurrence) : query.OrderBy(x => x.Recurrence),
                "createdat" => sortDescending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
                "updatedat" => sortDescending ? query.OrderByDescending(x => x.UpdatedAt) : query.OrderBy(x => x.UpdatedAt),
                _ => sortDescending ? query.OrderByDescending(x => x.Recurrence) : query.OrderBy(x => x.Recurrence) // Default sorting
            };

            // Get total item count for pagination
            int totalItems = await query.CountAsync();

            // Calculate total pages
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Apply paging
            query = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // Project to LayoutView and execute the query
            var items = await query.Select(a => new ScheduleView
            {
                ScheduleId = a.ScheduleId,
                LayoutId = a.LayoutId,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                Priority = a.Priority,
                PriorityValue = a.PriorityValue,
                LayoutName = a.LayoutName,
                CreatedBy = a.CreatedBy,
                Recurrence = a.Recurrence,
                UpdatedAt = a.UpdatedAt,
                CreatedAt = a.CreatedAt,
            }).ToListAsync();

            // Return paginated result
            return new PaginatedResult<ScheduleView>
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages
            };

        }

        public async Task<ScheduleView> GetById(int id)
        {
            var query = await (from a in _context.Schedules
                               join layout in _context.Layouts
                               on a.LayoutId equals layout.LayoutId
                               where a.ScheduleId == id
                               select new ScheduleView
                               {
                                   ScheduleId = a.ScheduleId,
                                   LayoutId = a.LayoutId,
                                   StartTime = a.StartTime,
                                   EndTime = a.EndTime,
                                   Priority = a.Priority,
                                   LayoutName = layout.Name,
                                   CreatedBy = a.CreatedBy,
                                   Recurrence = a.Recurrence,
                                   UpdatedAt = a.UpdatedAt,
                                   CreatedAt = a.CreatedAt
                               }).FirstOrDefaultAsync();
            return query;
        }


        public async Task<ScheduleView> DeleteById(int id)
        {
            // Fetch the user from the database
            Schedule a = await _context.Schedules.FirstOrDefaultAsync(u => u.ScheduleId == id);

            // Check if the user exists
            if (a == null)
            {
                // Handle user not found case (throw exception or return null)
                return null; // Or throw an exception
            }
            else
            {
                var scheduleView = new ScheduleView
                {
                    ScheduleId = a.ScheduleId,
                    LayoutId = a.LayoutId,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    Priority = a.Priority,
                    CreatedBy = a.CreatedBy,
                    Recurrence = a.Recurrence,
                    UpdatedAt = a.UpdatedAt,
                    CreatedAt = a.CreatedAt
                };
                _context.Schedules.Update(a);
                _context.SaveChanges();
                return scheduleView;

            }

        }
    }
}
